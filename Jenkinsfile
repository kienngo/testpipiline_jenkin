pipeline {
    agent any 

    stages {
        stage('Build') { 
            steps { 
                sh 'dotnet clean'
				sh 'dotnet build' 
            }
        }
    }
}