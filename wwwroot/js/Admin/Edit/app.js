$(document).ready(function () {
    new Vue({
        el: '#main-container',
        store: store,
        computed: {
            post() {
                return this.$store.state.post;
            },
            postVersion() {
                return this.$store.state.postVersion;
            },
            parsedText() {
                return this.$store.state.postVersion.rawText;
            },
            tab() {
                return this.$store.state.tab;
            },
            saved() {
                return this.$store.state.saving.saved;
            }
        },
        data: {
            scaleWidth: 1
        },
        methods: {
            setTab: function (value) {
                this.$store.commit('setTab', value);
            },
            updateScale: function () {
                this.$nextTick(function () {
                    let container = document.getElementById('parsed-container');
                    if (container) {
                        this.scaleWidth = container.clientWidth / 960;
                    }
                });
            },
            resetSave: function () {
                this.$store.dispatch('resetSave');
            }
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
                window.addEventListener("resize", this.updateScale);
                document.querySelectorAll('pre code').forEach((block) => {
                    hljs.highlightBlock(block)
                })
                this.updateScale();
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