namespace ManyToMany2.Controllers {

    export class HomeController {
        public movies;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.$http.get('/api/movies').then((response) => {
                this.movies = response.data;
            })
        }
    }

    export class AddMovieController {
        public actors;
        public movie;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.$http.get('/api/actors').then((response) => {
                this.actors = response.data;
            })
        }
        public addMovie() {
            this.$http.post('/api/movies/', this.movie).then((response) => {
                this.$state.go('home');
            })
        }
    }

    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
