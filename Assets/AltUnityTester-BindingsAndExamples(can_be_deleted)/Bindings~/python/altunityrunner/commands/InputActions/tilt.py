from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements
import time
class Tilt(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd,x, y, z):
        super().__init__(socket,requestSeparator,requestEnd)
        self.x=x
        self.y=y
        self.z=z
    
    def execute(self):
        acceleration = self.vector_to_json_string(x, y, z)
        print ('Tilt with acceleration: ' + acceleration)
        data = self.send_data(self.create_command('tilt', acceleration ))
        return self.handle_errors(data)