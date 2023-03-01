
async function addNewTag() {
    document.getElementById("newTagId").insertAdjacentHTML(
        "beforeend",
        `<div class="mb-3">
          <label class="control-label">New Tag</label>
          <input name="Tags" class="form-control" value="" />
          <span class="text-danger"></span>
      </div>`
    );

}