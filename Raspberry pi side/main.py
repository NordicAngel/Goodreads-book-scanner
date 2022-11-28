import evdev

scancodes = {
    # Scancode: ASCIICode
    2: u'1', 3: u'2', 4: u'3', 5: u'4', 6: u'5', 7: u'6', 8: u'7', 9: u'8',
    10: u'9', 11: u'0', 28: u'CRLF'
}

def broadcast_isb(isbn):
    print(isbn)

dev = evdev.InputDevice('/dev/input/event1')

isbn = ""

for event in dev.read_loop():
    if event.type != evdev.ecodes.EV_KEY: #a guard agenst non key events
        continue

    data = evdev.catagorize(event)
    if data.keystate != 1: #guard agenst non down presses
        continue

    if data.scancode == 28: #if event is new line send isbn
        broadcast_isb(isbn)
        isbn = ""
        continue

    isbn += scancodes.get(data.scancode)