from altunityrunner.commands.base_command import BaseCommand
class GetText(BaseCommand):
    def __init__(self, socket,requestSeparator,requestEnd,alt_object):
        super().__init__(socket,requestSeparator,requestEnd)
        self.alt_object=alt_object
    
    def execute(self):
        property_info = '{"component":"UnityEngine.UI.Text", "property":"text"}'
        data = self.send_data(self.create_command('getObjectComponentProperty', self.alt_object,  property_info  ))
        return self.handle_errors(data)
