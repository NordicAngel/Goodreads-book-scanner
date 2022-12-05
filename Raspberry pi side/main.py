import evdev
import asyncio
import websockets

scancodes = {
    # Scancode: ASCIICode
    2: u'1', 3: u'2', 4: u'3', 5: u'4', 6: u'5', 7: u'6', 8: u'7', 9: u'8',
    10: u'9', 11: u'0', 28: u'CRLF'
}

#Finds the correct device
devices = [evdev.InputDevice(path) for path in evdev.list_devices()]
for device in devices:
    if device.phys == "usb-20980000.usb-1/input0":
        dev = device


async def read_isbn(websocket):
    isbn = ""
    for event in dev.read_loop():
        if event.type != evdev.ecodes.EV_KEY: #a guard against non key events
            continue

        data = evdev.categorize(event)
        if data.keystate != 1: #guard against non down presses
            continue

        if data.scancode == 28: #if event is new line send isbn
            await broadcast_isb(websocket, isbn)
            isbn = ""
            break

        isbn += scancodes.get(data.scancode) #add character to the variable


async def broadcast_isb(websocket, isbn):
    await websocket.send(isbn)


#Opens a websocketserver that returns one isbn
async def main():
    async with websockets.serve(read_isbn, "0.0.0.0", 12000):
        await asyncio.Future()


asyncio.run(main())
