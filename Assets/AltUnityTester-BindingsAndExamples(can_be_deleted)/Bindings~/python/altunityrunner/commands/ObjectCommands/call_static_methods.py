from altunityrunner.commands.base_command import BaseCommand
class CallStaticMethods(BaseCommand):
    def __init__(self, socket,requestSeparator,requestEnd,type_name, method_name, parameters, type_of_parameters = '',assembly=''):
        super().__init__(socket,requestSeparator,requestEnd)
        self.type_name=type_name
        self.method_name=method_name
        self.parameters=parameters
        self.type_of_parameters=type_of_parameters
        self.assembly=assembly
    
    def execute(self):
        action_info = '{"component":"' + self.type_name + '", "method":"' + self.method_name + '", "parameters":"' + self.parameters + '", "typeofparameters":"' + self.type_of_parameters +'", "assembly":"'+self.assembly+'"}'
        data=self.send_data(self.create_command("callComponentMethodForObject","",action_info))
        return self.handle_errors(data)