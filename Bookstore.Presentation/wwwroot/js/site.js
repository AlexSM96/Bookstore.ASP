function showPatial(url) {
    const model = document.querySelector('#div-admin')
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => model.innerHTML = response,
        error: (response) => console.log(response)
    })
}

$(document).ready(function () {
    const commentContainer = document.querySelector('#comment-container')
    const { bookId } = commentContainer.dataset

    $.ajax({
        type: 'POST',
        url: '/Review/GetComments',
        data: { bookId },
        success: (response) => commentContainer.innerHTML = response,
        error: (response) => console.log(response)
    })
})

$(document).ready(function () {
    const dropdownList = document.querySelector('.dropdown-menu')

    $.ajax({
        type: 'GET',
        url: '/Category/GetCategories',
        success: (response) => dropdownList.innerHTML = response,
        error: (response) => console.log(response)
    })
})

let currentUser;
$(document).ready(async function () {
    const data = await fetch('/User/GetCurrentUser')
    currentUser = await data.json()
})


function createOrder(data) {
    if (currentUser === null || currentUser === undefined) {
        alert("Чтобы совершить покупку необходимо войти в аккаунт или зарегистрироваться");
        return
    }
    let books = JSON.parse(data)

    $.ajax({
        type: 'POST',
        url: '/Order/CreateOrder',
        data: {
            userId: currentUser.userId,
            books: books 
        },
        success: () => alert("Заказ создан. Спаибо за покупку!"),
        error: (response) => console.log(response)
    })
}

let booksId = []
function getBooksId(id) {
    if (!booksId.includes(id)) {
        booksId.push(id)
    }
}


function addToBasket() {
    let basketContainer = document.querySelector('#main-container')

    $.ajax({
        type: 'POST',
        url: '/Book/AddBooksToBasket',
        data: { booksId },
        success: (response) => basketContainer.innerHTML = response,
        error: (response) => console.log(response)
    })
}









