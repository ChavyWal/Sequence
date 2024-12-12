
namespace tsequence{ 
const randomLetter = () => String.fromCharCode(Math.random() * (85 - 65) + 65)
const l = [...document.querySelectorAll(".spot")]
const s: HTMLButtonElement = document.querySelector("#startgame");
const r: HTMLButtonElement = document.querySelector("#ruReady");
const imagebtn: HTMLButtonElement[] = [...document.querySelectorAll<HTMLButtonElement>('.btnimage')];
const msg = document.querySelector("#msg");
const round = document.querySelector("#round");
const score = document.querySelector("#score");
const answer = document.querySelector("#answer");
let btnspressed: string[] = [];
let spotText: string[] = [];
let n = 0;
let roundnum = 0;
let scorenum = 0;
r.disabled = true;
s.addEventListener("click", startGame);
r.addEventListener("click", readytoguess);
imagebtn.forEach(i => {
    i.addEventListener("click", btnPress)
    i.disabled = true;
});

function btnPress(event: Event) {
    n++
    let btn = event.target as HTMLElement;
    const imgBox: HTMLElement = document.querySelector(`#s${n}`);
    imgBox.classList.add("spotimage");
    imgBox.textContent = btn.textContent;
    imgBox.classList.remove("spotGuess");
    btnspressed.push(btn.textContent);
    if (n > 3) {
        imagebtn.forEach(i => i.disabled = true);
        checkIfRight();
    }
}

function checkIfRight() {
    n = 0;
    if (btnspressed.join('') === spotText.join('')) {
        msg.textContent = `GREAT JOB YOU GOT IT RIGHT!!!!!!!`
        score.textContent = `score # ${++scorenum}`;
        l.forEach(l => {
            l.classList.add("spotCorrect");
        })
    }
    else {
        msg.textContent = `I AM SORRY BUT THE IMAGES YOU HAVE ENTERED ARE NOT CORRECT!!!!!!!`;
        l.forEach(l => {
            l.classList.add("spotIncorrect");
        })
    }
    answer.textContent = spotText.join(' ');
    btnspressed = [];
    spotText = [];
    s.disabled = false;
}

function startGame() {
    l.forEach(l => {
        l.textContent = randomLetter();
        l.classList.add("spotimage");
        l.classList.remove("spotGuess");
        l.classList.remove("spotCorrect");
        l.classList.remove("spotIncorrect");
        spotText.push(l.textContent);
    })
    answer.textContent = "";
    round.textContent = `round # ${++roundnum}`;
    imagebtn.forEach(i => i.disabled = true);
    s.disabled = true;
    r.disabled = false;
    msg.textContent = "Memorize the images in the correct order.";
};

function readytoguess() {
    l.forEach(l => {
        l.textContent = "";
        l.classList.remove("spotimage");
        l.classList.add("spotGuess");
    })
    imagebtn.forEach(i => i.disabled = false);
    s.disabled = true;
    r.disabled = true;
    msg.textContent = "Enter the correct images in the order that you memorized them."
}
}
