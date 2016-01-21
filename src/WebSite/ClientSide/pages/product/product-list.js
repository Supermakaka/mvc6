require('datatables-extended');
require('datatables-filters');
require('datatables-filters-daterangepicker');
require('datatables-filters-iCheck');
require('bootstrap-daterangepicker');
require('bootstrap-datepicker');
require('bootstrap-datepicker/dist/css/bootstrap-datepicker3.css');

var moment = require('moment');
var actionsColumnTemplate = require('./product-list-column-actions.hbs');
var enabledColumnTemplate = require('./product-list-column-enabled.hbs');
var site = require('site/site');

var table;

function initTable() {
    table.DataTable({
        ajax: { url: '/Product/AdminProductListAjax' },
        columnDefs: [
            { 'width': "10%", 'targets': [0] }
        ],
        columns: [
            { data: 'id', name: 'Id', sortable: true },
            { data: 'name', name: 'Name', sortable: true },
            { data: 'Price', name: 'Price', sortable: true },
            { data: 'count', name: 'Count', sortable: true },
            { data: 'productCategoryName', name: 'ProductCategoryName', sortable: false },
            { data: 'productSubCategoryName', name: 'ProductSubCategoryName', sortable: true },
            {
                data: 'visible', name: 'Visible', sortable: true,
                render: function (data, type, full, meta) {
                    return enabledColumnTemplate({ disabled: data });
                }
            },
            { data: 'createDate', name: 'CreateDate', sortable: true },
            {
                data: function (row, type, val, meta) {
                    return actionsColumnTemplate({ id: row.id, disabled: row.disabled });
                },
                sortable: false
            }
        ]
    });
}

function initActions() {
    $('#create-date-filter').daterangepicker({
        autoUpdateInput: false,
        opens: 'left',
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        locale: { cancelLabel: 'Clear' }
    });

    site.ajaxLink('.btn-user-disable', {
        url: '/Admin/DisableOrEnableUser',
        data: function (el) {
            return { id: el.data('id') };
        },
        success: function () {
            refreshTable(false);
        }
    });

    site.modalFormLink('#btn-user-create', {
        url: '/Admin/CreateUser',
        formSubmitSuccess: function () {
            refreshTable(false);
        }
    });

    site.modalFormLink('.btn-user-edit', {
        url: '/Admin/EditUser',
        data: function (el) {
            return { id: el.data('id') };
        },
        formSubmitSuccess: function () {
            refreshTable(false);
        }
    });

    site.modalFormLink('.btn-user-delete', {
        url: '/Admin/ConfirmDeleteUser',
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