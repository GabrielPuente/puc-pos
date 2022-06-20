/*
# Globais
*/
var page = 1;
var detailView = document.getElementById("content");
var results = document.getElementById("results");

var endpointMovies = "https://api.themoviedb.org/3/movie/popular?api_key=1f54bd990f1cdfb230adb312546d765d&language=pt-BR&page=";
var endpointGenre = "https://api.themoviedb.org/3/genre/movie/list?api_key=1f54bd990f1cdfb230adb312546d765d&language=pt-BR";
var urlSmallImage = "https://www.themoviedb.org/t/p/w220_and_h330_face";
var urlLargeImage = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2";
var urlBackgroudImage = "https://image.tmdb.org/t/p/w1920_and_h800_multi_faces";

var dataJson = [];
var allGenres;

var CACHE = "Movies";
var CACHEGENRES = "Genres";

/*
# Requisição AJAX
*/


GetAllGenre();
LoadingMovies();

function LoadingMovies() {

    let ajax = new XMLHttpRequest();

    ajax.open("GET", endpointMovies + page, true);
    ajax.send();

    //Lendo requisição
    ajax.onreadystatechange = function () {

        if (this.readyState == 4 && this.status == 200) {

            var response = JSON.parse(this.responseText).results;
            response.forEach(x => dataJson.push(x));

            if (dataJson.length > 0) {
                cacheJson();
                results.className = "row";
                ShowMovies();
                page++;
            }
        }
    }
}

function GetAllGenre() {

    let ajax = new XMLHttpRequest();

    ajax.open("GET", endpointGenre, true);
    ajax.send();

    //Lendo requisição
    ajax.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            allGenres = JSON.parse(this.responseText).genres;
            cacheJsonGenres();
        }
    }
}

function ShowMovies() {

    let htmlContent = "";

    for (let i = (page - 1) * 20; i < dataJson.length; i++) {
        htmlContent += CardView(dataJson[i].id, dataJson[i].vote_average, urlSmallImage + dataJson[i].poster_path);
    }
    results.innerHTML += htmlContent;
}

/*
# Comportamento Botões
*/

let btnBack = document.getElementById("btnBack");

btnBack.addEventListener("click", function () {
    detailView.className = "animate__animated animate__bounceOutLeft";
    detailView.style.display = "none";
    document.getElementById("backgroud-detail").style.display = "none";

    setTimeout(function () { document.getElementById("content_img").style.display = "none"; }, 500);
});

let btnLoadingMore = document.getElementById("btnLoadingMore");

btnLoadingMore.addEventListener("click", function () {
    LoadingMovies();
});

function ViewDetail(id) {
    detailView.style.display = "block";
    document.getElementById("backgroud-detail").style.display = "block";

    var movie = dataJson.find(element => element.id == id);

    var genres = getGenres(movie);
    var genresText = genres.join([separador = ', ']);

    var date = new Date(movie.release_date);
    let dateFormated = (AddZero(date.getDate().toString()) + "/" + (AddZero(date.getMonth() + 1).toString()) + "/" + date.getFullYear());

    document.getElementById("content_img").style.display = "block";
    document.getElementById("content_img").setAttribute("src", urlLargeImage + movie.poster_path);

    detailView.style.backgroundImage = "url(" + urlBackgroudImage + movie.backdrop_path + ")";
    detailView.className = "animate__animated animate__bounceInLeft";

    document.getElementById("contentName").innerHTML = movie.title;
    document.getElementById("contentReleaseDate").innerHTML = "Lançado em: " + dateFormated;
    document.getElementById("ContentVote").innerHTML = movie.vote_average;
    document.getElementById("contentGenres").innerHTML = genresText;
    document.getElementById("contentSinopse").innerHTML = movie.overview;
}

function getGenres(movie) {
    var genres = [];
    movie.genre_ids.forEach(genre => {
        genres.push(allGenres.find(x => x.id == genre).name);
    });

    return genres;
}

function CardView(id, voteAverage, urlImg) {

    return '<div class="col-12 col-md-2 col-margin-left card-movie" style="background-image: url(' + urlSmallImage + urlImg + ');" onClick="javascript:ViewDetail(\'' + id + '\');" data-id="' + id + '">' +
        '<span class="badge rounded-pill bg-alert-poke vote-average">' + voteAverage + '</span>' +
        '</div>';
}

var cacheJson = function () {
    localStorage[CACHE] = JSON.stringify(dataJson);
}

var cacheJsonGenres = function () {
    localStorage[CACHEGENRES] = JSON.stringify(allGenres);
}

let windowInstallation = null;

const btnInstall = document.getElementById("btnInstall");

window.addEventListener('beforeinstallprompt', SaveWindow);

function SaveWindow(evt) {
    windowInstallation = evt;
}

let InitInstallation = function () {

    btnInstall.removeAttribute("hidden");
    btnInstall.addEventListener("click", function () {

        windowInstallation.prompt();

        windowInstallation.userChoice.then((choice) => {

            if (choice.outcome === 'accepted') {
                console.log("Usuário fez a instalação do app");
            } else {
                console.log("Usuário NÃO fez a instalação do app");
            }

        });

    });
}

function AddZero(number) {
    if (number <= 9)
        return "0" + number;
    else
        return number;
}

function padLeadingZeros(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}