var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
        postVersion: null,
        allTags: null,
        tab: 'edit',
        saving: { saved: true, timeout: null },
        newTag: { 'name': '' },
        selectedTag: {'name': ''}
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value.post;
            state.postVersion = value.postVersion;
            if (state.postVersion.rawText.length > 0) {
                state.postVersion.rawText = state.postVersion.rawText.substr(state.postVersion.previewText.length + 1);
            }
            else {
                state.postVersion.rawText = state.postVersion.rawText.substr(state.postVersion.previewText.length);
            }
            state.allTags = value.tags;
        },
        setTab: (state, value) => {
            state.tab = value;
        },
        setSaved: (state, value) => {
            state.saving.saved = value
        },
        setTimeout: (state, value) => {
            state.saving.timeout = value;
        },
        addExternalFile: (state, value) => {
            state.post.externalFiles.push(value);
        },
        removeExternalFile: (state, value) => {
            state.post.externalFiles = state.post.externalFiles.filter(function (obj) {
                return obj.fileName != value;
            });
        },
        removePostTag: (state, value) => {
            state.post.postTags = state.post.postTags.filter(function (tag) {
                return tag != value;
            });
        },
        addTag: (state, value) => {
            state.allTags.push(value);
            state.newTag.name = '';
        },
        addPostTag: (state, value) => {
            state.post.postTags.push(value);
            state.selectedTag.name = '';
        }
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
        resetSave: (context) => {
            context.commit('setSaved', false);
            if (context.state.saving.timeout) {
                clearTimeout(context.state.saving.timeout);
                context.commit('setTimeout', null);
            }
            context.commit('setTimeout', setTimeout(function () {
                context.dispatch('saveText');
            }, 5000));
        },
        saveText: (context) => {
            context.commit('setTimeout', null);
            let payload = {
                postVersionId: context.state.postVersion.postVersionId,
                rawText: context.state.postVersion.previewText+'\n'+context.state.postVersion.rawText,
                previewText: context.state.postVersion.previewText
            };
            $.ajax({
                type: "put",
                url: "/api/Post/PostVersion",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('setSaved', true);
                },
                error: function (result) {
                    alert("Could not save");
                }
            });
        },
        deleteExternalFile: (context, value) => {
            $.ajax({
                url: '/api/ExternalFile/ExternalFile/' + value,
                type: 'DELETE',
                success: function (result) {
                    context.commit('removeExternalFile', value);
                },
                error: function () {
                    alert('Could not remove file');
                }
            });
        },
        deletePostTag: (context, value) => {
            let tags = context.state.post.postTags.filter(function (tag) {
                return tag != value;
            });
            let data = { 'postTags': tags, 'postId': context.state.post.postId };
            $.ajax({
                url: '/api/Post/Post',
                type: 'PUT',
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    context.commit('removePostTag', value);
                },
                error: function () {
                    alert('Could not remove post tag');
                }
            });
        },
        createTag: (context) => {
            let data = {
                'name': context.state.newTag.name
            };
            $.ajax({
                url: '/api/Tag/Tag/',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function (result) {
                    context.commit('addTag', context.state.newTag.name);
                },
                error: function () {
                    alert('Could not create tag');
                }
            });
        },
        addPostTag: (context) => {
            let tags = JSON.parse(JSON.stringify(context.state.post.postTags));
            tags.push(context.state.selectedTag.name)
            let data = { 'postId': context.state.post.postId, 'postTags': tags };
            $.ajax({
                url: '/api/Post/Post',
                type: 'PUT',
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (result) {
                    context.commit('addPostTag', context.state.selectedTag.name);
                },
                error: function () {
                    alert('Could not add post tag');
                }
            });
        }
    }
});