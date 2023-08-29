const selector = document.querySelector('#authorsSelect')

async function getData() {
    try {
        const response = await fetch('/Author/GetJsonListAuthors')
        const data = await response.json()
        render(data)
    } catch (e) {
    }
}

getData()

function render(authors = []) {
    const html = authors.map(toHtml)
    selector.innerHTML = html
}

function toHtml(author) {
    return `<option class="">${author.name}</option>`
}