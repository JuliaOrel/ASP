let currentIndex = 0;

function addSessionHandler(index) {
    if (currentIndex === 0) {
        currentIndex = index;

    }
    document.getElementById("sessionsId").insertAdjacentHTML(
        "beforeend",
        `<div class="mb-3">
<label for=>New Session</label>
<input name="Sessions[${currentIndex}].TimeSession" class="form-control"/>
</div>`
    );
    currentIndex++;
}