import React from 'react'

function Home() {
    return (
        <div>
            <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                <img class="d-block w-100" src="carousel1.jpg"/>
                </div>
                <div class="carousel-item">
                <img class="d-block w-100" src="carousel2.jpg"/>
                </div>
                <div class="carousel-item">
                <img class="d-block w-100" src="carousel4.jpg"/>
                </div>
            </div>
            <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
            </div>
        </div>
    )
}

export default Home
