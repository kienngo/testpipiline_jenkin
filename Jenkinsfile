pipeline {
    agent any
	environment {  
		dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
	}  

    stages {
        stage('Build') { 
            steps { 
                sh 'dotnet clean'
				sh 'dotnet build' 
            }
        }
    }
}