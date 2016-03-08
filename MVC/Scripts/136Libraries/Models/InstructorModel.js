function InstructorModel() {

    var people = [];
    var initialLoad = false;

    var NewPerson = function (fn, ln, t) {
        this.firstname = fn;
        this.lastname = ln;
        this.title = t;
    }

    this.Load = function (callback) {
        //Do AJAX call
        //Here is fake data
        //if (!initialLoad) {
        //    initialLoad = true;
        //    people.push(new NewPerson("Alice", 20));
        //   people.push(new NewPerson("Bob", 21));
        //    people.push(new NewPerson("Ken", 19));
        //}
        $.ajax({
            method: 'GET',
            url: "http://localhost:9393/api/Instructor/GetInstructorList",
            data: "",
            dataType: "json",
            success: function (result) {          
                callback(result);
            },
            error: function () {         
                alert('Error while loading admin info.');
                callback("Error while loading admin info");
            }
        });
    };

    this.Insert = function (data, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/Instructor/InsertIntructor",
            data: data,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading admin info.');
                callback("Error while loading admin info");
            }
        });
    }


    this.Delete = function (data, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/Instructor/DeleteInstructor",
            data: data,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading admin info.');
                callback("Error while loading admin info");
            }
        });
    }

    this.Update = function (instructor, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/api/Instructor/Update",
            data: instructor,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading admin info.');
                callback("Error while loading admin info");
            }
        });
    }
}
