from altunityrunner.commands.command_returning_alt_elements import CommandReturningAltElements
from altunityrunner.by import By
class FindObjectsWhichContains(CommandReturningAltElements):
    def __init__(self, socket,requestSeparator,requestEnd,by,value,camera_name,enabled):
        super().__init__(socket,requestSeparator,requestEnd)
        self.by=by
        self.value=value
        self.camera_name=camera_name
        self.enabled=enabled
    
    def execute(self):
        path=self.set_path_contains(self.by,self.value)
        if self.enabled==True:
            data = self.send_data(self.create_command('findObjects', path , self.camera_name ,'true'))
        else:
            data = self.send_data(self.create_command('findObjects', path , self.camera_name ,'false'))
        return self.get_alt_elements(data)