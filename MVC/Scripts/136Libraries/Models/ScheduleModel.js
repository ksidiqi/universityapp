    function ScheduleModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.CreateSchedule = function (schedule, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:9393/Api/Schedulle/InsertSchedule",
                data: schedule,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding schedule.  Is your service layer running?');
                }
            });
        };

        this.Load = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: 'GET',
                url: "http://localhost:9393/Api/Schedulle/GetScheduleByScheduleId?schedule_id=" + id,
                data: "",
                dataType: "json",
                success: function (result)
                {
                   
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule detail.  Is your service layer running?');
                }
            });
        };

        this.DeleteSchedule = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:9393/Api/Schedulle/DeleteSchedule?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting schedule.  Is your service layer running?');
                }
            });
        };

        this.GetAll = function (year, quarter, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:9393/Api/Schedulle/GetScheduleList?year=" + year + "&quarter=" + quarter,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading schedule list.  Is your service layer running?');
                }
            });
        };

        this.Update = function (scheduleData, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:9393/Api/Schedulle/UpdateSchedule",
                data: scheduleData,
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating schedule info');
                }
            });
        };
    }