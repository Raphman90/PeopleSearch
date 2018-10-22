const uri = 'api/person';
let people = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'person';
    if (data) {
        if (data > 1) {
            name = 'people';
        }
        el.text(data + ' ' + name);
    } else {
        el.text('No Results' );
    }
}

$(document).ready(function () {
    $('#results').hide();
});


function slowsearch() {
    $('#people').empty();
    $('#results').hide();
    $('#loader').show();
    setTimeout('search()', 3000);
}

function search() {
    $('#loader').hide();
    $.ajax({
        type: 'GET',
        url: uri + "/search/" + $('#searchName').val(),
        success: function (data) {
            $('#people').empty();

            if (data.length > 0) {
                $('#results').show();
            }
            else {
                $('#results').hide();
            }

            getCount(data.length);
            $.each(data, function (key, item) {

                var imgLocation = (item.imageLocation != null) ? item.imageLocation : "/images/unknown.jpg";

                $('<tr> <td><img src="' + imgLocation + '"style="height: 30px; width 30px;"/></td>' +
                    '<td> ' + item.id + '</td> ' +
                    '<td> ' + item.firstName + ' ' + item.lastName + '</td > ' +
                    '<td>' + item.age + '</td>' +
                    '<td>' + item.address + '</td>' +
                    '<td>' + item.interests + '</td>' +
                    '<td><button onclick="editItem(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + item.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#people'));
            });

            people = data;
        }
    });
}

function addItem() {
    const item = {
        'FirstName': $('#add-first-name').val(),
        'LastName': $('#add-last-name').val(),
        'Age': $('#add-age').val(),
        'Address': $('#add-address').val(),
        'Interests': $('#add-interests').val(),
        'ImageLocation': $('#add-image-location').val()
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
        },
        success: function (result) {
            search();
            $('#add-first-name').val('');
            $('#add-last-name').val('');
            $('#add-age-name').val('');
            $('#add-address').val('');
            $('#add-interests').val('');
            $('#add-image-location').val('');
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            search();
        }
    });
}

function editItem(id) {
    $.each(people, function (key, item) {
        if (item.id === id) {
            $('#edit-first-name').val(item.firstName);
            $('#edit-last-name').val(item.lastName);
            $('#edit-age').val(item.age);
            $('#edit-address').val(item.address);
            $('#edit-interests').val(item.interests);
            $('#edit-id').val(item.id);
            $('#edit-image-location').val(item.imageLocation);
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const item = {
        'FirstName': $('#edit-first-name').val(),
        'LastName': $('#edit-last-name').val(),
        'Age': $('#edit-age').val(),
        'Address': $('#edit-address').val(),
        'Interests': $('#edit-interests').val(),
        'ImageLocation': $('#edit-image-location').val(),
        'Id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            search();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}