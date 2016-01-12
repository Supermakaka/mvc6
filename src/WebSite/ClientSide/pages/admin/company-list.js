require('datatables-extended');
require('datatables-filters');
require('datatables-filters-daterangepicker');

var actionsColumnTemplate = require('./company-list-actions.hbs');
var site = require('site/site');

var table;

function initTable() {
    table.DataTable({
        ajax: { url: '/Admin/CompanyListAjax' },
        columnDefs: [
            { 'width': "10%", 'targets': [0] }
        ],
        columns: [
            { data: 'id', name: 'Id', sortable: true },
            { data: 'name', name: 'Name', sortable: true },
            {
                data: function (row, type, val) {
                    return actionsColumnTemplate({ id: row.id });
                },
                sortable: false
            }
        ]
    });
}

function initActions() {

    site.modalFormLink('#btn-company-create', {
        url: '/Admin/CreateCompany',
        formSubmitSuccess: function () {
            refreshTable(false);
        }
    });

    site.modalFormLink('.btn-company-edit', {
        url: '/Admin/EditCompany',
        data: function (el) {
            return { id: el.data('id') };
        },
        formSubmitSuccess: function () {
            refreshTable(false);
        }
    });

    site.modalFormLink('.btn-company-delete', {
        url: '/Admin/ConfirmDeleteCompany',
        data: function (el) {
            return { id: el.data('id') };
        },
        formSubmitSuccess: function () {
            refreshTable(true);
        }
    });
}

function refreshTable(resetPaging) {
    table.DataTable().draw(resetPaging);
}

module.exports.onReady = function (t) {
    table = t;

    initTable();
    initActions();
};