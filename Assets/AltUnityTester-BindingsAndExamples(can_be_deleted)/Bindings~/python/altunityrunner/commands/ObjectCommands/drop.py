from altunityrunner.commands.base_command import BaseCommand
class Drop(BaseCommand):
    def __init__(self, socket,requestSeparator,requestEnd,alt_object):
        super().__init__(socket,requestSeparator,requestEnd,x,y)
        self.x=x
        self.y=y
        self.alt_object=alt_object
    
    def execute(self):
        position_string = self.vector_to_json_string(x, y)
        data = self.send_data(self.create_command('dropObject', position_string , self.alt_object ))
        return self.handle_errors(data)