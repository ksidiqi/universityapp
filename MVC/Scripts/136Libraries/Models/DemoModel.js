function DemoModel() {

    var people = [];
    var initialLoad = false;

    var NewPerson = function (n, a) {
        this.name = n;
        this.age = a;
    }

    this.Load = function (callback) {
        //Do AJAX call
        //Here is fake data
        if (!initialLoad) {
            initialLoad = true;
            people.push(new NewPerson("Alice", 20));
            people.push(new NewPerson("Bob", 21));
            people.push(new NewPerson("Ken", 19));
        }

        callback(people);
    };

    this.Insert = function (data, callback) {
        people.push(data);
        callback("ok");
    }

    this.Delete = function (data, callback) {
        var index = people.indexOf(data);
        if (index > -1) {
            people.splice(index, 1);
            callback("ok");
        }
        
        callback("error");
    }
}
