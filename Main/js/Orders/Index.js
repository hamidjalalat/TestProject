


const app = new Vue({

    el: "#app",
    data: {
        selectGroupId: null,
        groupList: null,
        editParameters: {
            id:0,
            name: null,
            price: null,
            description: null,
            available: null,
            groupProductId: null,
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

            this.editParameters.groupProductId = this.selectGroupId;
          
            axios.post('/order/Edit', this.editParameters)
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
            this.editParameters.id = id;
            axios.post('/order/GetInfoEdit', this.id)

                .then(response => {
                    console.log(response.data.listGruopProduct);
                    this.editParameters.name = response.data.listProduct.Name;
                    this.editParameters.price = response.data.listProduct.Price;
                    this.editParameters.description = response.data.listProduct.Description;
                    this.editParameters.available = response.data.listProduct.Available;
                    //this.editParameters.groupProductId = response.data.listProduct.GroupProductId;
                    this.selectGroupId = response.data.listProduct.GroupProductId;
                    this.groupList = response.data.listGruopProduct;

                })
                .catch(error => {

                    console.log(error)

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
