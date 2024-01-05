pipeline {
    agent any
	environment {  
		dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
	}  

    stages {
        stage('Build') { 
            steps {
				bat 'dotnet nuget add source https://pkgs.dev.azure.com/PJC-Dev-Organization/PJC-Dev-Prj/_packaging/PJC-Dev-Artifacts/nuget/v3/index.json'
				bat 'dotnet restore %WORKSPACE%\\PJC-SV-GroupSettings\\Wiseman.PJC.Service.GroupSettings.sln'
				bat 'dotnet build %WORKSPACE%\\PJC-SV-GroupSettings\\Wiseman.PJC.Service.GroupSettings.sln --configuration Release'
            }
        }
    }
}