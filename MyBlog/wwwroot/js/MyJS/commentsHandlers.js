async function mainCommentHandler(event) {

    event.preventDefault();
    let form = document.forms.mainCommentForm;
    let message = form.elements.Message.value;
    let postId = form.elements.postId.value;

    const response = await fetch("/Comments/CreateComment", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            message: message,
            postId: postId
        })
    });

    if (response) {

        recountComments(1);
        const writeParentCommentAndChildrenPartial = await response.text();
        document.getElementById("mainCommentId").innerHTML += writeParentCommentAndChildrenPartial;
    }
    else {
        console.log(response.statusText);
    }
}

function recountComments(count) {

    let commentsCountElement = document.getElementById("commentsCountId");
    let commentsCount = commentsCountElement.innerText;
    commentsCount = +commentsCount + count;
    commentsCountElement.innerText = commentsCount;
}
