import evdev
import asyncio
import websockets

scancodes = {
    # Scancode: ASCIICode
    2: u'1', 3: u'2', 4: u'3', 5: u'4', 6: u'5', 7: u'6', 8: u'7', 9: u'8',
    10: u'9', 11: u'0', 28: u'CRLF'
}




dev = evdev.InputDevice('/dev/input/event1')


async def hello(websocket):
    isbn = ""
    for event in dev.read_loop():
        if event.type != evdev.ecodes.EV_KEY: #a guard agenst non key events
            continue

        data = evdev.categorize(event)
        if data.keystate != 1: #guard agenst non down presses
            continue

        if data.scancode == 28: #if event is new line send isbn
            await broadcast_isb(websocket, isbn)
            isbn = ""
            continue

        isbn += scancodes.get(data.scancode)


async def broadcast_isb(websocket, isbn):
    await websocket.send("Hello world!")
    print(isbn)


async def main():
    async with websockets.serve(hello, "localhost", 12000):
        await asyncio.Future()


asyncio.run(main())
