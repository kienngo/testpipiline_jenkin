pipeline {
    agent any
	environment {  
		dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
	}  

    stages {
        stage('Build') { 
            steps {
				sh 'dotnet build %WORKSPACE%\\PJC-SV-GroupSettings\\Wiseman.PJC.Service.GroupSettings.sln --configuration Release' 
            }
        }
    }
}