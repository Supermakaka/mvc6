//var site = require('./site.js');
require('./jquery.validate.unobtrusive.bootstrap.js');

function getModal(modalSelector)
{
    return $(modalSelector == null ? "#modal" : modalSelector);
}

function hide(modalSelector) {
    getModal(modalSelector).modal('hide');
}

function show(modalSelector) {
    getModal(modalSelector).modal('show');
}

function setContent(content, modalSelector) {
    var modal = getModal(modalSelector);

    modal.find('.modal-content').html(content);

    if (modal.find('form').length > 0)
        modal.find('form').validateBootstrap(true);

    //if (modal.find('input').length > 0)
        //site.initCheckboxes(modal);
}

module.exports.hide = hide;
module.exports.show = show;
module.exports.setContent = setContent;
module.exports.getModal = getModal;