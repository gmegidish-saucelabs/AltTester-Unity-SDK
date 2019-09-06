from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements
import time
class ScrollMouse(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd, speed, duration):
        super().__init__(socket,requestSeparator,requestEnd)
        self.speed=speed
        self.duration=duration
    
    def execute(self):
        print ('Scroll mouse with: ' + str(self.speed))
        data = self.send_data(self.create_command('scrollMouse', self.speed, self.duration ))
        return self.handle_errors(data)