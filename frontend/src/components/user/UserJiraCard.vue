<template>
    <div class="bg-gray-800 p-5 rounded-3xl shadow-xl mb-8 w-[35%]">
        <h3 class="text-xl font-semibold text-white mb-4 border-b border-blue-500 pb-2">
            Visualizar Card Jira
        </h3>

        <div class="mb-6">
            <div class="flex space-x-3">
                <input 
                    type="text" 
                    class="flex-1 bg-gray-900 p-3 border border-gray-700 rounded-lg focus:ring-indigo-500 focus:border-indigo-500 transition duration-150 shadow-sm text-gray-100" 
                    v-model="jiraKey" 
                    placeholder="Digite a chave (Ex: SW-123)"
                    @keyup.enter="searchJira"
                >
                <button 
                    class="px-5 py-3 text-white bg-indigo-600 rounded-lg hover:bg-indigo-700 disabled:bg-indigo-400 transition duration-150 font-medium shadow-md flex items-center justify-center cursor-pointer" 
                    @click="searchJira" 
                    :disabled="isLoading"
                >
                    <svg v-if="isLoading" class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                        <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                        <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                    </svg>
                    <span v-else>Buscar</span>
                </button>
            </div>
        </div>

        <div v-if="jiraIssues.length > 0" class="mt-6 border-t pt-4">
            <div class="space-y-4 max-h-88 overflow-y-auto pr-2">
                <div v-for="issue in jiraIssues" :key="issue.key" class="bg-gray-700 border-l-4 border-blue-500 p-3 rounded-lg shadow-md h-35">
                    <div class="flex justify-between items-start mb-1">
                        <h5 class="text-md font-bold text-blue-500">{{ issue.key }}</h5>
                        <span :class="['px-2 py-0.5 text-xs font-semibold rounded-full', getStatusBadgeClass(issue.status)]">
                            {{ issue.status }}
                        </span>
                    </div>
                    <h6 class="text-sm font-medium text-white mb-2">{{ issue.summary }}</h6>
                    
                    <p class="text-sm text-gray-200 whitespace-pre-wrap">{{ issue.description }}</p>

                    </div>
            </div>
        </div>
        
        </div>
</template>

<script setup>
import { ref } from 'vue';
import { getJiraIssueDetails, searchJiraIssues } from '../../services/employeeApi';

const jiraKey = ref('');
const jiraIssues = ref([]);
const isLoading = ref(false);

function getStatusBadgeClass(status) {
    if (!status) return 'bg-gray-400 text-gray-800';
    const normalizedStatus = status.toLowerCase();
    if (normalizedStatus.includes('to do') || normalizedStatus.includes('open')) return 'bg-blue-200 text-blue-800';
    if (normalizedStatus.includes('in progress')) return 'bg-yellow-200 text-yellow-800';
    if (normalizedStatus.includes('done') || normalizedStatus.includes('closed')) return 'bg-green-200 text-green-800';
    return 'bg-gray-200 text-gray-800';
}

async function searchJira() {
    if (!jiraKey.value) return;
    
    isLoading.value = true;
    jiraIssues.value = []; 

    const isExactKey = /^[A-Z]+-\d+$/.test(jiraKey.value.toUpperCase());
    let data;

    try {
        if (isExactKey) {
            data = await getJiraIssueDetails(jiraKey.value.toUpperCase());
            
            if (data && data.key) {
                jiraIssues.value = [data]; 
            }
        } else {
            data = await searchJiraIssues(jiraKey.value.toUpperCase());
            
            if (Array.isArray(data)) {
                jiraIssues.value = data;
            }
        }
    } catch (error) {
        jiraIssues.value = [];
    } finally {
        isLoading.value = false;
    }
}
</script>