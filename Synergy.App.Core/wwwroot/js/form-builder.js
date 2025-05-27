// Notification handling
function showNotification(type, duration = 3000) {
    const notification = document.getElementById(`${type}Notification`);
    notification.classList.add('show');
    setTimeout(() => {
        notification.classList.remove('show');
    }, duration);
}

// Template Form Builder
const templateForm = {
    components: [
        {
            type: 'textfield',
            key: 'name',
            label: 'Name',
            placeholder: 'Enter template name',
            validate: {
                required: true
            }
        },
        {
            type: 'textfield',
            key: 'code',
            label: 'Code',
            placeholder: 'Enter template code',
            validate: {
                required: true
            }
        },
        {
            type: 'textarea',
            key: 'description',
            label: 'Description',
            placeholder: 'Enter template description'
        },
        {
            type: 'select',
            key: 'tableMetadataId',
            label: 'Table Metadata',
            dataSrc: 'url',
            data: {
                url: '/api/tablemetadata'  // You'll need to implement this endpoint
            },
            valueProperty: 'id',
            template: '{{ item.name }}',
            validate: {
                required: true
            }
        },
        {
            type: 'textarea',
            key: 'json',
            label: 'JSON',
            placeholder: 'Enter template JSON',
            editor: 'ace',
            validate: {
                required: true
            }
        },
        {
            type: 'button',
            action: 'submit',
            label: 'Submit',
            theme: 'primary'
        }
    ]
};

// Table Metadata Form Builder
const tableMetadataForm = {
    components: [
        {
            type: 'textfield',
            key: 'code',
            label: 'Code',
            placeholder: 'Enter table code',
            validate: {
                required: true
            }
        },
        {
            type: 'textfield',
            key: 'name',
            label: 'Name',
            placeholder: 'Enter table name',
            validate: {
                required: true
            }
        },
        {
            type: 'textarea',
            key: 'description',
            label: 'Description',
            placeholder: 'Enter table description'
        },
        {
            type: 'textfield',
            key: 'alias',
            label: 'Alias',
            placeholder: 'Enter table alias'
        },
        {
            type: 'textfield',
            key: 'schema',
            label: 'Schema',
            placeholder: 'Enter schema name'
        },
        {
            type: 'checkbox',
            key: 'createTable',
            label: 'Create Table'
        },
        {
            type: 'textarea',
            key: 'query',
            label: 'Query',
            placeholder: 'Enter SQL query',
            editor: 'ace'
        },
        {
            type: 'button',
            action: 'submit',
            label: 'Submit',
            theme: 'primary'
        }
    ]
};

// Column Metadata Form Builder
const columnMetadataForm = {
    components: [
        {
            type: 'textfield',
            key: 'name',
            label: 'Name',
            placeholder: 'Enter column name',
            validate: {
                required: true
            }
        },
        {
            type: 'textfield',
            key: 'labelName',
            label: 'Label Name',
            placeholder: 'Enter label name'
        },
        {
            type: 'textfield',
            key: 'alias',
            label: 'Alias',
            placeholder: 'Enter column alias'
        },
        {
            type: 'select',
            key: 'dataType',
            label: 'Data Type',
            data: {
                values: [
                    { label: 'String', value: 'String' },
                    { label: 'Integer', value: 'Integer' },
                    { label: 'Decimal', value: 'Decimal' },
                    { label: 'DateTime', value: 'DateTime' },
                    { label: 'Boolean', value: 'Boolean' }
                ]
            },
            validate: {
                required: true
            }
        },
        {
            type: 'select',
            key: 'udfUIType',
            label: 'UDF UI Type',
            data: {
                values: [
                    { label: 'TextBox', value: 'TextBox' },
                    { label: 'TextArea', value: 'TextArea' },
                    { label: 'Dropdown', value: 'Dropdown' },
                    { label: 'Checkbox', value: 'Checkbox' },
                    { label: 'DatePicker', value: 'DatePicker' }
                ]
            }
        },
        {
            type: 'checkbox',
            key: 'isNullable',
            label: 'Is Nullable'
        },
        {
            type: 'checkbox',
            key: 'isPrimaryKey',
            label: 'Is Primary Key'
        },
        {
            type: 'checkbox',
            key: 'isForeignKey',
            label: 'Is Foreign Key'
        },
        {
            type: 'checkbox',
            key: 'isSystemColumn',
            label: 'Is System Column'
        },
        {
            type: 'select',
            key: 'tableMetadataId',
            label: 'Table Metadata',
            dataSrc: 'url',
            data: {
                url: '/api/tablemetadata'  // You'll need to implement this endpoint
            },
            valueProperty: 'id',
            template: '{{ item.name }}',
            validate: {
                required: true
            }
        },
        {
            type: 'button',
            action: 'submit',
            label: 'Submit',
            theme: 'primary'
        }
    ]
};

// Initialize Form Builders
Formio.builder(document.getElementById('templateBuilder'), templateForm);
Formio.builder(document.getElementById('tableMetadataBuilder'), tableMetadataForm);
Formio.builder(document.getElementById('columnMetadataBuilder'), columnMetadataForm);

// Handle form submissions
document.addEventListener('DOMContentLoaded', () => {
    const forms = {
        template: null,
        tableMetadata: null,
        columnMetadata: null
    };

    // Event listener for form submissions
    ['template', 'tableMetadata', 'columnMetadata'].forEach(formType => {
        const element = document.getElementById(`${formType}Builder`);
        Formio.builder(element, eval(`${formType}Form`)).then(builder => {
            forms[formType] = builder;

            builder.on('submitDone', (submission) => {
                console.log(`${formType} submission:`, submission);
                // Here you would typically send the data to your backend
                fetch(`/api/${formType.toLowerCase()}`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(submission.data)
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(data => {
                    console.log('Success:', data);
                    showNotification('success');
                    builder.reset();
                })
                .catch((error) => {
                    console.error('Error:', error);
                    showNotification('error');
                });
            });
        });
    });
});


