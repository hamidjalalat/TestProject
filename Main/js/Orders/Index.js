const app = new Vue({

    el: "#app",
    data: {
        listProduct: null,
        //labelPageIndex: null,
        //labelPageCount: null,

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

            if (intCount % this.product.pageSize == 0) {
                return ((intCount / this.product.pageSize) - 1);
            }
            else {
                return (intCount / this.product.pageSize);
            }
        },
        firstButtonClick: function myfunction() {
            alert("firstButtonClick");
            this.product.pageIndex = 0;
            this.onSerche();
        },
        previousButtonClick: function () {
            alert("previousButtonClick");
            if (this.product.pageIndex > 0) {
                this.product.pageIndex --;
                alert("");
                this.onSerche();
            }
        },
   
        nextButtonClick: function myfunction() {
            alert("nextButtonClick");
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
           
            this.listProduct = null;
            axios.post('/order/GetProduct', this.product)

                .then(response => {

                    this.listProduct = response.data.data;
                 

                })
                .catch(error => {

                    console.error(error)

                })
                .finally(() => {

                })
        }
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

            })

         
    }

})
