from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements
from altunityrunner.player_pref_key_type import PlayerPrefKeyType
class SetPlayerPrefKey(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd,key_name, value, key_type):
        super().__init__(socket,requestSeparator,requestEnd)
        self.key_name=key_name
        self.value=value
        self.key_type=key_type
    
    def execute(self):
        data = ''
        if self.key_type is 1:
            print('Set Int Player Pref for key: ' + self.key_name + ' to ' + str(self.value))
            data = self.send_data(self.create_command('setKeyPlayerPref', self.key_name , str(self.value) , str(PlayerPrefKeyType.Int) ))
        if self.key_type is 2:
            print('Set String Player Pref for key: ' + self.key_name + ' to ' + str(self.value))
            data = self.send_data(self.create_command('setKeyPlayerPref', self.key_name , str(self.value) , str(PlayerPrefKeyType.String) ))
        if self.key_type is 3:
            print('Set Float Player Pref for key: ' + self.key_name + ' to ' + str(self.value))
            data = self.send_data(self.create_command('setKeyPlayerPref', self.key_name , str(self.value) , str(PlayerPrefKeyType.Float) ))
        return self.handle_errors(data)