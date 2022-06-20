'use strict';

const CACHE = "Movies";

const FILES_CACHE = [
    'css/animate.min.css',
    'css/bootstrap.min.css',
    'css/Army.ttf',
    'css/Army.woff',
    'css/styles.css',
    'imgs/arrow_left.png',
    'imgs/load.gif',
    'imgs/offline.png',
    'js/bootstrap.bundle.min.js',
    'off-line.html'
];

//Instalação e Ativação do PWA

self.addEventListener('install', (evt) =>{

    evt.waitUntil(

        caches.open(CACHE).then((cache) => {

            console.log("Service Worker: rregistrando caches estáticos");
            return cache.addAll(FILES_CACHE);

        })

    );

    self.skipWaiting();

});

self.addEventListener('activate', (evt) => {

    evt.waitUntil(
        
        caches.keys().then((keylist) => {

            return Promise.all(keylist.map((key) => {

                if(key !== CACHE){
                    return caches.delete(key);
                }

            }));

        })
    )
});

//Responder a Experiência Off-line

self.addEventListener('fetch', (evt)=>{

    if(evt.request.mode !== 'navigate'){
        return;
    }

    evt.respondWith(

        fetch(evt.request).catch(()=>{

            return caches.open(CACHE).then((cache) =>{

                return cache.match('off-line.html');

            });

        })

    );

});