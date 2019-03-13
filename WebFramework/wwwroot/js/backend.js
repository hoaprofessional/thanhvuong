
var onSorting = null;

$(document).ready(function () {
    // xoa loi khi nhap lieu lai input
    $('body').delegate('input.input-validation-error', 'input', function () {
        $(this).removeClass('input-validation-error');
        $(this).parent().find('.field-validation-error').remove();
    });

    $('body').delegate('thead th.sorting', 'click', function () {
        $(this).removeClass('sorting');
        $('thead th.sorting_asc').addClass('sorting');
        $('thead th.sorting_asc').removeClass('sorting_asc');
        $('thead th.sorting_desc').addClass('sorting');
        $('thead th.sorting_desc').removeClass('sorting_desc');
        $(this).addClass('sorting_asc');
        if (onSorting != null) {
            onSorting();
        }
    });

    $('body').delegate('thead th.sorting_asc', 'click', function () {
        $(this).removeClass('sorting_asc');
        $(this).addClass('sorting_desc');
        if (onSorting != null) {
            onSorting();
        }
    });

    $('body').delegate('.editable', 'keyup', function (e) {
        if (e.keyCode === 13) {
            $(this).html($(this).html() + '<br>');
        }
    });

    $('body').delegate('thead th.sorting_desc', 'click', function () {
        $(this).removeClass('sorting_desc');
        $(this).addClass('sorting_asc');
        if (onSorting != null) {
            onSorting();
        }
    });
});

var uniqId = (function () {
    var i = 0;
    return function () {
        return i++;
    }
})();

function uploadFile(selector, url, success, fail) {
    var formData = new FormData();
    formData.append('file', $(selector)[0].files[0]); // myFile is the input type="file" control


    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: success,
        error: fail,
        complete: function (jqXHR, status) {
        }
    });

}

function getFormattedDate(date) {
    var month = format(date.getMonth() + 1);
    var day = format(date.getDate());
    var year = format(date.getFullYear());
    return day + "/" + month + "/" + year;
}

var s = 'some+multi+word+string'.replace(/\+/g, ' ');

if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

String.prototype.replaceAllText = function (char, newchar) {
    return this.replace(new RegExp(char, 'g'), newchar);
};

function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
}

jQuery.fn.insertAt = function (index, element) {
    var lastIndex = $(this).children().length;
    if (index < 0) {
        index = Math.max(0, lastIndex + 1 + index);
    }
    this.append(element);
    if (index < lastIndex) {
        this.children().eq(index).before(this.children().last());
    }
    return this;
}

function jsonToFormData(inJSON, inTestJSON, inFormData, parentKey) {
    // http://stackoverflow.com/a/22783314/260665
    // Raj: Converts any nested JSON to formData.
    var form_data = inFormData || new FormData();
    var testJSON = inTestJSON || {};
    for (var key in inJSON) {
        // 1. If it is a recursion, then key has to be constructed like "parent.child" where parent JSON contains a child JSON
        // 2. Perform append data only if the value for key is not a JSON, recurse otherwise!
        var constructedKey = key;
        if (parentKey) {
            constructedKey = parentKey + "." + key;
        }

        var value = inJSON[key];
        if (value && value.constructor === {}.constructor) {
            // This is a JSON, we now need to recurse!
            jsonToFormData(value, testJSON, form_data, constructedKey);
        } else {
            form_data.append(constructedKey, inJSON[key]);
            testJSON[constructedKey] = inJSON[key];
        }
    }
    return form_data;
}


//collapse script
var coll = document.getElementsByClassName("collapsible");
var i;
for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}
//collapse script end

$('#open-filter-button,.open-filter-button').click(function (e) {
    e.preventDefault();
    $('.filter-area[data-page=' + $(this).attr('data-href') + '] #filter-button').click();
});

function fixPrice(priceString) {
    return priceString.replaceAllText(',', '')
        .replaceAllText('đ', '')
        .replaceAllText(' ', '');
}


$(function () {
    $('.auto-complete-content').each(function () {
        var objData = {};
        $(this).find('select option').each(function () {
            objData[$(this).text()] = null;
        });
        $(this).find('input').autocomplete({
            data: objData,
            limit: 20, // The max amount of results that can be shown at once. Default: Infinity.
            onAutocomplete: function (val) {
                if (typeof (autoComplete) != 'undefined') {
                    autoComplete(this);
                }
            },
            minLength: 1, // The minimum length of the input for the autocomplete to start. Default: 1.
        });
    });
});

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

var coll = document.getElementsByClassName("collapsible");
var i;
for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var parent = this.parentElement;

        if (parent.style.height) {
            parent.style.height = null;
            parent.classList.remove("expanded")
        } else {
            parent.style.height = "85vh";
            parent.classList.add("expanded")
        }
    });
}

function toDataUrl(url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        var reader = new FileReader();
        reader.onloadend = function () {
            callback(reader.result);
        }
        reader.readAsDataURL(xhr.response);
    };
    xhr.open('GET', url);
    xhr.responseType = 'blob';
    xhr.send();
}

function resetDatePicker() {
    $('.datepicker:not(.picker__input)').each(function () {
        if ($(this).closest('.picker').length == 0) {
            $(this).pickadate({
                monthsFull: ['Tháng 1',
                    'Tháng 2',
                    'Tháng 3',
                    'Tháng 4',
                    'Tháng 5',
                    'Tháng 6',
                    'Tháng 7',
                    'Tháng 8',
                    'Tháng 9',
                    'Tháng 10',
                    'Tháng 11',
                    'Tháng 12'],
                weekdaysShort: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                monthsShort: ['Tháng 1',
                    'Tháng 2',
                    'Tháng 3',
                    'Tháng 4',
                    'Tháng 5',
                    'Tháng 6',
                    'Tháng 7',
                    'Tháng 8',
                    'Tháng 9',
                    'Tháng 10',
                    'Tháng 11',
                    'Tháng 12'],
                today: 'Hôm nay',
                clear: 'Hủy chọn',
                format: 'dd/mm/yyyy',
                formatSubmit: 'dd/mm/yyyy'
            });
        }
    });
}

function fixDateString(str) {
    var day = str.substr(0, str.indexOf('/'));
    str = str.replace(day + '/', '');
    var month = str.substr(0, str.indexOf('/'));
    str = str.replace(month + '/', '');
    var year = str;
    return year + '/' + month + '/' + day;
}

$('#notifications-dropdown li.notification-content a').click(function (e) {
    e.preventDefault();
});

$('#task-card li.notification-content a').click(function (e) {
    e.preventDefault();
});

$('#notifications-dropdown li').click(function (e) {
    var t = $(this);
    if ($(this).hasClass('new')) {
        $.post('/Home/MakeOldNotification?notificationId=' + $(this).attr('data-id')).done(function (response) {
            if (response.result == "success") {
                window.location.href = t.find('a').attr('href');
            }
        });
    }
    else {
        window.location.href = t.find('a').attr('href');
    }
});

$('body').delegate('#task-card li.notification-content', 'click', function (e) {
    var t = $(this);
    //window.open(t.find('a').attr('href'));
    $(t).removeClass('email-unread');
    if ($(this).hasClass('email-unread')) {
        $.post('/Home/MakeOldNotification?notificationId=' + $(this).attr('data-id')).done(function (response) {
            if (response.result == "success") {
            }
        });
    }
});