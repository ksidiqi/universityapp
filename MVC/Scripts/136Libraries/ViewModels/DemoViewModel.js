function DemoViewModel1() {
    var self = this;
    var demoModelObj = new DemoModel();

    var staticViewModel = {
        name: "Bob",
        job: "Student",
        age: 20
    };

    this.Initialize = function () {
        ko.applyBindings(staticViewModel, document.getElementById("divLogin"));
    };
};

function DemoViewModel2() {
    var self = this;
    var demoModelObj = new DemoModel();

    var observablesViewModel = {
        name: ko.observable("Alice"),
        job: ko.observable("Student"),
        age: ko.observable(20)
    }

    this.Initialize = function () {
        ko.applyBindings(observablesViewModel, document.getElementById("divLogin"));
    };
};

function DemoViewModel3() {
    var self = this;
    var demoModelObj = new DemoModel();

    var observablesViewModel = {
        click: ko.observable(0),
        updateClick: function (data) {
            update(data);
        }
    }

    var update = function (data) {
        data.click(data.click() + 1);
    }

    this.Initialize = function () {
        ko.applyBindings(observablesViewModel, document.getElementById("divLogin"));
    };
};

function DemoViewModel4() {
    var self = this;
    var demoModelObj = new DemoModel();

    var observablesViewModel = {
        itemToAdd: ko.observable(""),
        allItems: ko.observableArray(["Fries", "Eggs Benedict", "Ham", "Cheese"]),
        selectedItems: ko.observableArray(["Ham"]),
        addItem: function () {
            if ((this.itemToAdd() != "") && (this.allItems.indexOf(this.itemToAdd()) < 0)) // Prevent blanks and duplicates
                this.allItems.push(this.itemToAdd());
            this.itemToAdd("");
        },
        removeSelected: function () {
            this.allItems.removeAll(this.selectedItems());
            this.selectedItems([]);
        },
        sortItems: function () {
            this.allItems.sort();
        }
    }

    this.Initialize = function () {
        ko.applyBindings(observablesViewModel, document.getElementById("divDemoContent"));
    };
};

function DemoViewModel5()
{
    var self = this;
    var initialBind = false;
    var demoModelObj = new DemoModel();

    var observablesViewModel = {
        newPerson: {
            name: ko.observable(''),
            age: ko.observable('')
        },
        people: ko.observableArray(),
        addNewPerson: function (data) {
            addNewPerson(data);
        },
        deletePerson: function (data) {
            deletePerson(data);
        }
    };

    var deletePerson = function (data) {
        demoModelObj.Delete(data, function (result) {
            if (result == 'ok') {
                self.Initialize();
            }
        });
    };

    var addNewPerson = function(data) {
        var personData = {
            name: observablesViewModel.newPerson.name(),
            age: observablesViewModel.newPerson.age()
        };
        
        demoModelObj.Insert(personData, function (result) {
            if (result == 'ok') {
                self.Initialize();
                observablesViewModel.newPerson.name('');
                observablesViewModel.newPerson.age('');
            }
        });
    }

    this.Initialize = function () {
        demoModelObj.Load(function (result) {
            observablesViewModel.people(result);
            
        });

        if (!initialBind) {
            ko.applyBindings(observablesViewModel, document.getElementById("divDemoContent"));
            initialBind = true;
        }
    };
};