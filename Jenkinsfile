pipeline {

    agent any

    
    stages {

        stage('Packaging') {

            steps {
                
                sh 'docker build --pull --rm -f Dockerfile -t convocation2023:latest .'
                
            }
        }

        stage('Push to DockerHub') {

            steps {
                withDockerRegistry(credentialsId: 'dockerhub', url: 'https://index.docker.io/v1/') {
                    sh 'docker tag convocation2023:latest chalsfptu/convocation2023:latest'
                    sh 'docker push chalsfptu/convocation2023:latest'
                }
            }
        }

        stage('Deploy Spring Boot to DEV') {
            steps {
                echo 'Deploying and cleaning'
                sh 'docker image pull chalsfptu/convocation2023:latest'
                sh 'docker container stop fubloglogin || echo "this container does not exist" '
                sh 'echo y | docker container prune '
                sh 'docker container run -d --rm --name fubloglogin -p 82:80 -p 444:443  chalsfptu/convocation2023 '
            }
        }
        
 
    }
    post {
        // Clean after build
        always {
            cleanWs()
        }
    }
}
