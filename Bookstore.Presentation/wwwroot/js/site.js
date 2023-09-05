function showPatial(url) {
    const model = document.querySelector('#div-admin') 
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => { model.innerHTML = response },
        error: (response) => console.log(response)
    })
}