using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CL.Data.Repository;

public class MedicoRepository : IMedicoRepository
{
    private readonly CLContext _context;

    public MedicoRepository(CLContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Medico>> GetMedicosAsync()
    {
        return await _context.Medicos
          .Include(p => p.Especialidades)
          .AsNoTracking().ToListAsync();
    }

    public async Task<Medico> GetMedicoAsync(int id)
    {
        return await _context.Medicos
            .Include(p => p.Especialidades)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Medico> InsertMedicoAsync(Medico medico)
    {
        await InsertMedicoEspecilidades(medico);
        await _context.Medicos.AddAsync(medico);
        await _context.SaveChangesAsync();
        return medico;
    }

    private async Task InsertMedicoEspecilidades(Medico medico)
    {
        var especialidadesConsultadas = new List<Especialidade>();
        foreach (var especialidade in medico.Especialidades)
        {
            var especialidadeConsultada = await _context.Especialidades.FindAsync(especialidade.Id);
            especialidadesConsultadas.Add(especialidadeConsultada);
        }
        medico.Especialidades = especialidadesConsultadas;
    }

    public async Task<Medico> UpdateMedicoAsync(Medico medico)
    {
        var medicoConsultado = await _context.Medicos
            .Include(p => p.Especialidades)
            .SingleOrDefaultAsync(p => p.Id == medico.Id);
        if (medicoConsultado == null) return null;
        _context.Entry(medicoConsultado).CurrentValues.SetValues(medico); // Atribui as propriedade de um objeto a um outro identico.
        await UpdateMedicoEspecialidades(medico, medicoConsultado);
        await _context.SaveChangesAsync();
        return medicoConsultado;
    }

    private async Task UpdateMedicoEspecialidades(Medico medico, Medico medicoConsultado)
    {
        medicoConsultado.Especialidades.Clear();
        foreach (var especialidade in medico.Especialidades)
        {
            var especialidadeConsultada = await _context.Especialidades.FindAsync(especialidade.Id);
            medicoConsultado.Especialidades.Add(especialidadeConsultada);
        }
    }

    public async Task DeleteMedicoAsync(int id)
    {
        var medicoConsultado = await _context.Medicos.FindAsync(id);
        _context.Medicos.Remove(medicoConsultado);
        await _context.SaveChangesAsync();
    }
}
