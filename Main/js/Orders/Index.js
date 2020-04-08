const app = new Vue({

    el: "#app",
    data: {
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
        GetLastPageIndex: function () {
            let intCount = this.count;

            if (parseInt(intCount) % parseInt(this.product.pageSize) == 0) {

                return ((parseInt(intCount) / parseInt(this.product.pageSize)) - 1);
            }
            else {
                return (parseInt(intCount) / parseInt(this.product.pageSize));
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
            if (this.product.pageIndex < this.GetLastPageIndex()) {
                this.product.pageIndex++;
                this.onSerche();
            }
        },
        lastButtonClick: function () {
            this.product.pageIndex = this.GetLastPageIndex();

            this.onSerche();
        },
        onSerche: function () {
            this.labelPageIndex = (parseInt(this.product.pageIndex) + 1);
            this.labelPageCount = (parseInt( this.GetLastPageIndex()) + 1);


            this.listProduct = null;
            axios.post('/order/GetProduct', this.product)

                .then(response => {

                    this.listProduct = response.data.data;
                    //this.count = response.data.count;

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        }
    },
    computed: {
      
    },
    mounted() {
       
        axios.post('/order/GetListProduct')
    
            .then(response => {

                this.listProduct = response.data.data;
                this.count = response.data.count;

            })
            .catch(error => {

                console.error(error)

            })
            .finally(() => {
                this.labelPageIndex = (parseInt(this.product.pageIndex) + 1);
                this.labelPageCount = (parseInt(this.GetLastPageIndex()) + 1);
            })

         
    }

})
