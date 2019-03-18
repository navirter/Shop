﻿Vue.component('product-manager', {
    template: `<div>
                <div v-if="!editing">
                    <button class="button" @click="newProduct">Add New Product</button>
                    <table class="table">
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Value</th>
                        </tr>
                        <tr v-for="(product, index) in products">
                            <td>{{product.id}}</td>
                            <td>{{product.name}}</td>
                            <td>{{product.value}}</td>
                            <td><a @click="editProduct(product.id, index)">Edit</a></td>
                            <td><a @click="deleteProduct(product.id, index)">Remove</a></td>
                        </tr>
                    </table>
                </div>
                <div class="field" v-else>
                    <div class="control">
                        <label class="label">Product Name</label>
                        <input class="input" v-model="productModel.name" />
                    </div>
                    <div class="control">
                        <label class="label">Product Description</label>
                        <input class="input" v-model="productModel.description" />
                    </div>
                    <div class="control">
                        <label class="label">Product Value</label>
                        <input class="input" v-model="productModel.value" />
                    </div>
                    <button class="button is-success" @click="createProduct" v-if="!productModel.id">Create Product</button>
                    <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                    <button class="button" @click="cancel">Cancel</button>
                </div>
            </div>`,
    data: function () {
        return {
            editing: false,
            loading: false,
            objectIndex: 0,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                value: 1.99
            },
            products: []
        }
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct: function (id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getProducts: function () {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createProduct: function () {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateProduct: function () {
            this.loading = true;
            axios.put('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct: function (id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
        editProduct: function (id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;
        },
        cancel: function () {
            this.editing = false;
        }
    },
    computed: {

    }
});