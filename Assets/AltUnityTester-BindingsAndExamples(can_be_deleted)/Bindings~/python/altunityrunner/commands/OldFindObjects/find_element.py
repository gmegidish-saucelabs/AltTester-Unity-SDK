from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements
class FindElement(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd,value,camera_name,enabled):
        super().__init__(socket,requestSeparator,requestEnd)
        self.value=value
        self.camera_name=camera_name
        self.enabled=enabled
    
    def execute(self):
        if self.enabled==True:
            data = self.send_data(self.create_command('findObjectByName', self.value , self.camera_name ,'true'))
        else:
            data = self.send_data(self.create_command('findObjectByName', self.value , self.camera_name ,'false'))

        return self.get_alt_element(data)