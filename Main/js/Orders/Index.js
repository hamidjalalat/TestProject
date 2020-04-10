
Vue.component(`hj-modal`, {

   props: ['message','idmodal'],
    data: function () {

        return ({
            id: this.idmodal,
        })
    },
    
    template:
        ` <div class="modal" v-bind:id="id" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>{{message}}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">بستن</button>
                    </div>
                </div>
            </div>
        </div>`,

})


const app = new Vue({

    el: "#app",
    data: {
        edit: {
            id:0,
            name: null,
            price: null,
            description:null,
        },
        id: {
            idDelete: 0,
            idEdit: 0,

        },
        listProduct: null,
        labelPageIndex: null,
        labelPageCount: null,
        
        count:null,
        product: {
            pageSize :10,
            pageIndex : 0,
            name: null,
            price: null,
            description: null,
        },
    },
    methods: {
        editeProduct: function () {
            axios.post('/order/Edit', this.edit)

                .then(response => {
                    if (response.data) {
                        $(`div#editModal`).modal('hide');
                        $(`div#messagemodal`).modal();
                        this.onSerche();
                    }
                    else {
                        $(`div#loadingModal`).modal('hide');
                        $(`div#errordelete`).modal();
                    } 

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        },
        editshow: function (id) {
            this.id.idEdit = id;
            this.edit.id = id;
            axios.post('/order/GetInfoEdit', this.id)

                .then(response => {
                    console.log(response.data)
                    this.edit.name = response.data.Name;
                    this.edit.price = response.data.Price;
                    this.edit.description = response.data.Description;
                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
            $(`div#editModal`).modal();
            
        },
    
        confirmDelete: function () {
            axios.post('/order/DeleteProduct', this.id)

                .then(response => {
                    if (response.data) {
                        $(`div#loadingModal`).modal('hide');
                        $(`div#messagemodal`).modal();
                        this.onSerche();
                    }
                    else {
                        $(`div#loadingModal`).modal('hide');
                        $(`div#errordelete`).modal();
                    } 

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        },
        deleteProducts: function (id) {

            this.id.idDelete = id;
            $(`div#loadingModal`).modal();

        },
        GetLastPageIndex: function () {

            //alert("GetLastPageIndex");
            let intCount = this.count;

            if ((parseInt(intCount) % parseInt(this.product.pageSize) )== 0) {

                return ((parseInt(intCount) / parseInt(this.product.pageSize)) - 1);
            }
            else {
                return Math.floor( (parseInt(intCount) / parseInt(this.product.pageSize)));
            }
        },
   
        firstButtonClick: function () {

            this.product.pageIndex = 0;
            this.onSerche();

        },
        previousButtonClick: function () {

            if (this.product.pageIndex > 0) {
                this.product.pageIndex--;
                this.onSerche();
            }

        },

        nextButtonClick: function () {
           
            if (this.product.pageIndex < (this.GetLastPageIndex())) {
                this.product.pageIndex++;
                this.onSerche();
            }
           
        },
        lastButtonClick: function () {

            this.product.pageIndex = this.GetLastPageIndex();
            this.onSerche();

        },
        setParameter: function () {
           
            this.product.pageSize = 10;
            this.product.pageIndex = 0;

        },
        onSerche: function () {
            
            this.listProduct = null;
            axios.post('/order/GetProduct', this.product)

                .then(response => {

                    this.listProduct = response.data.data;
                    this.count = response.data.count;
                    this.labelPageIndex = (parseInt(this.product.pageIndex) + 1);
                    this.labelPageCount = (parseInt(this.GetLastPageIndex()) + 1);

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        }

    },
    computed:{
     
},
    mounted() {

        this.onSerche();
    }

})
