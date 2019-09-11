from altunityrunner.altElement import AltElement
from altunityrunner.commands.base_command import BaseCommand
import json
BUFFER_SIZE = 1024
class CommandReturningAltElements(BaseCommand):
    def __init__(self, socket,requestSeparator=';',requestEnd='&'):
        self.requestSeparator=requestSeparator
        self.requestEnd=requestEnd
        self.socket=socket

    def get_alt_element(self, data):
        print(data)
        if (data != '' and 'error:' not in data):
            alt_el = None
            try:
                alt_el = AltElement(self, self.appium_driver, data)
            except:
                alt_el = AltElement(self, None, data)
            print('Element ' + alt_el.name + ' found at x:' + str(alt_el.x) + ' y:' + str(alt_el.y) + ' mobileY:' + str(alt_el.mobileY))
            return alt_el
        self.handle_errors(data)
        return None
    
    def get_alt_elements(self, data):
        if (data != '' and 'error:' not in data):
            alt_elements = []
            elements = []
            try:
                elements = json.loads(data)
            except:
                raise Exception("Couldn't parse json data: " + data)
            
            alt_el = None
            for i in range(0, len(elements)):
                try:
                    alt_el = AltElement(self, self.appium_driver, json.dumps(elements[i]))
                except:
                    alt_el = AltElement(self, None, json.dumps(elements[i]))
                    
                alt_elements.append(alt_el)
                print('Element ' + alt_el.name + ' found at x:' + str(alt_el.x) + ' y:' + str(alt_el.y) + ' mobileY:' + str(alt_el.mobileY))
            return alt_elements
            
        self.handle_errors(data)
        return None
