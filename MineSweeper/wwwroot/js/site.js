// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*
 * called in MineSweeperInputList View
*/ 
function deleteMineSweeperInput(id) {
    $.ajax({
        url: "/api/MineSweeperAPI/" + id,
        type: "DELETE",
        success: function (data) {
            location.reload();
        },
        error: function (errorThrown) {
        }
    });
}

function createMineSweeperInput(input) {
        $.post('', $('form').serialize(), function () {
        $.ajax({
            url: "/api/MineSweeperAPI",
            type: "Post",
            data: JSON.stringify({ "input": input }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.form-control').val('');
                $('.text-info').html('MineSweeper sample input created successfully.');
                $('.text-danger').html('');
            },
            error: function (errorThrown) {

                var error = JSON.parse(JSON.stringify(errorThrown));
                if (error.status === 422) {
                    var msg = JSON.stringify(error.responseText);
                    $('.text-danger').html('MineSweeper sample input failed to be created due to incorrect format of input');
                } else {
                    var msg = JSON.stringify(error.responseJSON.errors.input);
                    $('.text-danger').html(msg);
                }

            }
        });
    });
}

function mineSweeperInputCount() {
    $.ajax({
        type: "GET",
        url: "/minesweeper_inputcount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (count) {
            $('#totalMineSweeperInputListCount').append(count);
            
        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function

    });    
}

function mineSweeperInputListLoad() {
    $.ajax({
        type: "GET",
        url: "/api/MineSweeperAPI",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data));

            $.each(data, function (i, item) {
                var row =
                    "<tr> " +
                    "<td id='Id'>" + item.id + "</td>" +
                    "<td id='Input'> <pre>" + item.input.trim().replace('\n', '<br/>') + "</pre></td>" +
                    "<td id='RequestProcessTimes'>" + item.requestProcessTimes + "</td>" +
                    "<td id='AddedDate'>" + Date(item.addedDate, "dd-MM-yyyy") + "</td>" +
                    "<td id='ModifiedDate'>" + Date(item.modifiedDate, "dd-MM-yyyy") + "</td>" +
                    "<td id='IPAddress'>" + item.ipAddress + "</td>" +
                    "<td>" +
                    "<div class='form-group'>" +
                    "<button class='btn btn-link' id='delete' onclick='deleteMineSweeperInput(" + item.id + ");'>Delete</button>" +
                    "</div>" +
                    "</td>" +
                    "</tr>";
                $('#Table tbody').append(row);
            }); //End of foreach Loop

            console.log(data);
        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function

    });
}

