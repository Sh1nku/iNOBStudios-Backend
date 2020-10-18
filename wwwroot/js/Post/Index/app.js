$(document).ready(function () {
    new Vue({
        el: '#main-container',
        store: store,
        computed: {
            post() {
                return this.$store.state.post;
            }
        },
        data: {

        },
        methods: {

        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
                document.querySelectorAll('pre code').forEach((block) => {
                    hljs.highlightBlock(block)
                });
            });
        },
        updated: function () {
            this.$nextTick(function () {
                document.querySelectorAll('pre code').forEach((block) => {
                    if (block.childElementCount == 0) {
                        hljs.highlightBlock(block)
                    }
                });
            });
        }
    });
});