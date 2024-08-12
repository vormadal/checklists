'use client'
import { ChecklistDto, ChecklistType, CreateChecklistDto, IChecklistDto, ICreateChecklistDto } from '@/api/ApiClient'
import { Box, Button, Container, TextField } from '@mui/material'
import { useRouter } from 'next/navigation'
import React, { useState } from 'react'
import { useChecklists } from '../../../api/useChecklists'
import { useCreateChecklist } from '../../../api/useCreateChecklist'
import { ChecklistOverview } from '../../../components/ChecklistOverview'
import NavigationBar from '../../../components/navigation/NavigationBar'
import { useCopyChecklist } from '../../../api/useCopyChecklist'

interface Type {
  searchParams: {
    type?: ChecklistType
  }
}

function getTitle(type: ChecklistType) {
  switch (type) {
    case ChecklistType.Checklist:
      return 'New Checklist'
    case ChecklistType.Template:
      return 'New Template'
  }
  return 'New List'
}

export default function CreateChecklist({ searchParams }: Type) {
  const router = useRouter()
  const mutation = useCreateChecklist()
  const copyMutation = useCopyChecklist()
  const { data: templates } = useChecklists(ChecklistType.Template)
  const [checklist, setChecklist] = useState<ICreateChecklistDto>({
    title: '',
    type: searchParams.type || ChecklistType.Checklist
  })

  const handleTitleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setChecklist({ ...checklist, title: event.target.value })
  }

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    const result = await mutation.mutateAsync(checklist)
    console.log('result', result)
    router.push(`/checklist/${result.id}/edit`)
  }

  const createFromTemplate = async (template: IChecklistDto) => {
    const result = await copyMutation.mutateAsync({
      id: template.id,
      details: {
        newTitle: checklist.title,
        newType: checklist.type
      }
    })
    router.push(`/checklist/${result.id}/edit`)
  }

  return (
    <>
      <NavigationBar
        title={getTitle(checklist.type)}
        backUrl="/"
      />
      <Container>
        <Box
          component="form"
          onSubmit={handleSubmit}
          noValidate
          gap={1}
          sx={{ mt: 1 }}
        >
          <TextField
            margin="normal"
            required
            fullWidth
            id="title"
            label="Checklist Name"
            placeholder="Enter the checklist name"
            name="title"
            autoFocus
            value={checklist.title}
            onChange={handleTitleChange}
          />

          {!!templates?.length && (
            <ChecklistOverview
              checklists={templates}
              subheader="Use a template"
              onClick={createFromTemplate}
            />
          )}

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Save
          </Button>
        </Box>
      </Container>
    </>
  )
}
