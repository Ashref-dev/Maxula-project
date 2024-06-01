
window.onscroll = function () {

    var header = document.getElementById('header');

    if (header == null) { return }

    header.style.transition = 'background-color 0.4s ease';


    if ((window.scrollY || window.pageYOffSet) > 0) {
        header.classList.add('bg-white', 'shadow-xl');
    } else {
        header.classList.remove('bg-white', 'shadow-xl');
    }
};


//confetti yay
function frame() {
    confetti({
        particleCount: 130,
        spread: 70,
        origin: { y: 0.6 }
    })
}

function printInvoke() {
    window.print();
}