from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements

class DeletePlayerPref(CommandReturningAltElements):
    def __init__(self, socket,request_separator,request_end):
        super().__init__(socket,request_separator,request_end)
    
    def execute(self):
        print('Delete all Player Prefs')
        data = self.send_data(self.create_command('deletePlayerPref'))
        return self.handle_errors(data)