let authorIndex = 1
let categoryIndex = 1
function addAuthor() {
    const author = getAuthorHTML(authorIndex)
    authorIndex++
    $('#author-container').append(author)
}

function addCategory() {
    const category = getCategoryHTML(categoryIndex)
    $('#category-container').append(category)
    categoryIndex++
}

function getAuthorHTML(index) {
    return `
        <div class="form-group">
            <label asp-for="Authors[${index}].Name">${index + 1}.Автор<sup>*</sup></label>
            <input class="form-control" type="text" asp-for="Authors[${index}].Name" name="Authors[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Authors[${index}].Name"></span>
        </div>
    `
}

function getCategoryHTML(index) {
    return `
        <div class="form-group">
            <label asp-for="Categories[${index}].Name">${index + 1}.Категория(жанр)<sup>*</sup></label>
            <input class="form-control" type="text" asp-for="Categories[${index}].Name" name="Categories[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Categories[${index}].Name"></span>
        </div>
        `
}