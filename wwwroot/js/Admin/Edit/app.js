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
                return this.$store.state.postVersion.previewText+'\n'+this.$store.state.postVersion.rawText;
            },
            tab() {
                return this.$store.state.tab;
            },
            saved() {
                return this.$store.state.saving.saved;
            },
            newTag() {
                return this.$store.state.newTag;
            },
            allTags() {
                return this.$store.state.allTags;
            },
            selectedTag() {
                return this.$store.state.selectedTag;
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
                        this.scaleWidth = (container.clientWidth / 960);
                    }
                });
            },
            resetSave: function () {
                this.$store.dispatch('resetSave');
            },
            uploadFile: function () {
                let vue = this;
                let formData = new FormData();
                formData.append('rawFile', document.getElementById('file').files[0]);
                formData.append('postId', this.post.postId);
                $.ajax({
                    url: '/api/ExternalFile/ExternalFile',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        vue.$store.commit('addExternalFile', result);
                    },
                    error: function () {
                        alert('Could not upload file');
                    }
                });
            },
            removeFile: function (filename) {
                this.$store.dispatch('deleteExternalFile', filename);
            },
            deletePostTag: function (filename) {
                this.$store.dispatch('deletePostTag', filename);
            },
            createTag: function () {
                this.$store.dispatch('createTag');
            },
            addPostTag: function () {
                this.$store.dispatch('addPostTag');
            }
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
                window.addEventListener("resize", this.updateScale);
                window.addEventListener("mouseup", this.updateScale);
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