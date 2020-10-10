var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
        postVersion: null,
        tab: 'edit',
        saving: { saved: true, timeout: null }
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value.post;
            state.postVersion = value.postVersion;
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
            $.ajax({
                url: '/api/ExternalFile/ExternalFile/' + value,
                type: 'DELETE',
                success: function (result) {
                    state.post.externalFiles = state.post.externalFiles.filter(function (obj) {
                        return obj.fileName != value;
                    });
                },
                error: function () {
                    alert('Could not remove file');
                }
            });
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
                rawText: context.state.postVersion.rawText,
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
        }
    }
});