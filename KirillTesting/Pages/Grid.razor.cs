using KirillTesting.Models.Sprs;
using KirillTesting.Models;
using KirillTesting.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using DevExpress.Blazor;
using KirillTesting.ImportExport;
using Microsoft.JSInterop;
using KirillTesting.Components;

namespace KirillTesting.Pages
{
    public partial class Grid
    {
        private List<Person> _people;
        [Inject]
        public IPersonsRepository PersonRepository { get; set; }
        [Inject]
        public IGenderRepository GenderRepository { get; set; }
        [Inject]
        public IPolicyTypesRepository PolicyTypesRepository { get; set; }
        [Inject]
        PersonsExporter PersonsExporter { get; set; }
        [Inject]
        IJSRuntime JS { get; set; }
        MessageBox _messageBox = new MessageBox(); 

        private bool _uploadVisible = false;

        private IReadOnlyList<object> _selectedDataItems = new List<Person>();

        private List<Gender> _genders;
        private List<PolicyType> _policyTypes;
        protected override async Task OnInitializedAsync()
        {
            _genders = await GenderRepository.GetAllGendersAsync();
            _policyTypes = await PolicyTypesRepository.GetAllPolicyTypes();

            await LoadDataAsync();
        }

        private void Grid_CustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                var newPerson = (Person)e.EditModel;
                newPerson.Firstname = "Bob";
                newPerson.Lastname = "Chop";
                newPerson.Gender = 1;

                newPerson.Policy = new Policy() { Type = 3 };
            }
        }
        private async Task Grid_EditModelSaving(GridEditModelSavingEventArgs e)
        {
            var editablePerson = (Person)e.EditModel;
            if (e.IsNew)
                await PersonRepository.AddPersonAsync(editablePerson);
            else
                await PersonRepository.UpdatePersonAsync(editablePerson.Id,editablePerson);

            await LoadDataAsync();
        }
        private async Task Grid_DataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            var deletingPerson = e.DataItem as Person;
            await PersonRepository.DeletePersonAsync(deletingPerson.Id);

            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            _people = await PersonRepository.GetAllPersonsAsync();
        }

        private async Task OnDownloadClick()
        {
            if(!_selectedDataItems.Any())
            {
                _messageBox.Show("Не выбран ни один элемент!");
                StateHasChanged();
                return;
            }

            var stream = await PersonsExporter.ExportAsync(_selectedDataItems.ToList());
            await JS.InvokeVoidAsync("downloadFileFromStream", _selectedDataItems.Count + ".xml", stream.ToArray());
        }
        private async Task OnUploadClick()
        {
            _uploadVisible = true;
        }

        private async Task OnUploadVisibleChangedAsync(bool v)
        {
            _uploadVisible = v;
            await LoadDataAsync();
            StateHasChanged();
        }
    }
}

