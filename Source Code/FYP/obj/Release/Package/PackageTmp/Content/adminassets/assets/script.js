let prism = document.querySelector(".rec-prism");

function FT(){
  prism.style.transform = "translateZ(-212px) rotateY( -90deg)";
}
function BMT(){
  prism.style.transform = "translateZ(-100px)";
}
function ST(){
  //prism.style.transform = "translateZ(-138px) translateX(300px) rotateY( 90deg)";
    prism.style.transform = "translateZ(-450px) translateX(40px) rotateY(-180deg)";
}

function IEP() {
    prism.style.transform = "translateZ(-285px) translateX(-160px) rotateY( -90deg)";
}

//function VG() {
//    prism.style.transform = "translateZ(-450px) translateX(40px) rotateY(-180deg);";
//}

function showSubscribe(){
  prism.style.transform = "translateZ(-100px) rotateX( -90deg)";
}

function showContactUs(){
  prism.style.transform = "translateZ(-100px) rotateY( 90deg)";
}

function showThankYou(){
  prism.style.transform = "translateZ(-100px) rotateX( 90deg)";
}
