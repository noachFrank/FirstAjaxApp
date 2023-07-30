$(() => {
    console.log("hello")
    refreshTable();

    const modal = new bootstrap.Modal($('.modal')[0]);

    function refreshTable() {
        $("tbody tr").remove();
        $.get('/home/getpeople', function (people) {
            console.log("getting ppl")
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td>
                <button class='btn btn-warning edit' data-person-id='${person.id}'>Edit</button>
                <button class='btn btn-danger delete' data-person-id='${person.id}'>Delete</button>
            </td>
</tr>`)
            });
        });

       
       


    };

    $("#add-person").on("click", function () {
        $("#firstName").val("")
        $("#lastName").val("")
        $("#age").val("")
        $("#id").remove();
        $("#update-person").hide()
        $("#save-person").show()
        $(".modal-title").html("Add Person")
        modal.show();
    })

    $("#save-person").on("click", function () {
        console.log("adding")

        const firstName = $("#firstName").val()
        const lastName = $("#lastName").val()
        const age = $("#age").val()

        if (!firstName || !lastName || !age) {
            $("h6").remove();
            $(".modal-body").append(`<h6 style="color: red">*Please Fill All Fields</h6>`)
            return;
        }

        $.post("/home/add", { firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        })



    })

    $("tbody").on("click", ".edit", function () {
        console.log("getting person info")
        const id = $(this).data("person-id")

        $.get(`/home/update?id=${id}`, function (person) {

            $(".modal-body").append(`<input type="hidden" id="id" value="${id}" />`)
            $("#firstName").val(person.firstName)
            $("#lastName").val(person.lastName)
            $("#age").val(person.age)
            $("#update-person").show()
            $("#save-person").hide()
            $(".modal-title").html("Edit Person")
        })

        modal.show();
    })

    $("#update-person").on("click", function () {
        console.log("adding")
        const id = $("#id").val()
        const firstName = $("#firstName").val()
        const lastName = $("#lastName").val()
        const age = $("#age").val()

        if (!firstName || !lastName || !age) {
            $("h6").remove();
            $(".modal-body").append(`<h6 style="color: red">*Please Fill All Fields</h6>`)
            return;
        }

        $.post("/home/update", {id, firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        })



    })

    $("tbody").on("click", ".delete", function () {
        console.log("getting delete info")
        const id = $(this).data("person-id")

        $.post(`/home/delete?id=${id}`, function () {
            refreshTable();
        })
    });

});
