


const app = new Vue({

    el: "#app",
    data: {
        idDelete:null,
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
    
        
        deleteProducts: function (id) {
            alert(id);
            idDelete = id;
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
            //alert("firstButtonClick");
            this.product.pageIndex = 0;
            this.onSerche();
        },
        previousButtonClick: function () {
            //alert("previousButtonClick");
            if (this.product.pageIndex > 0) {
                this.product.pageIndex--;
                this.onSerche();
            }
        },

        nextButtonClick: function () {
            //alert("nextButtonClick");
            if (this.product.pageIndex == this.GetLastPageIndex()) {
               
            }
            if (this.product.pageIndex < (this.GetLastPageIndex())) {
                this.product.pageIndex++;
                this.onSerche();
            }
           
        },
        lastButtonClick: function () {
            //alert("lastButtonClick");
            this.product.pageIndex = this.GetLastPageIndex();

            this.onSerche();
        },
        setParameter: function () {
           
            this.product.pageSize = 10;

            this.product.pageIndex = 0;
        },
        onSerche: function () {
            
            //alert("onserch");
            
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
