from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements

class SetTimeScale(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd,time_scale):
        super().__init__(socket,requestSeparator,requestEnd)
        self.time_scale=time_scale
    
    def execute(self):
        print('Set time scale to: ' + str(self.time_scale))
        data = self.send_data(self.create_command('setTimeScale', str(self.time_scale)))
        if (data == 'Ok'):
            print('Time scale set to: ' + str(self.time_scale))
            return data
        return None